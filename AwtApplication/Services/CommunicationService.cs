using AwtApplication.Models;
using AwtApplication.Params;
using AwtApplication.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AwtApplication.Services
{
    public delegate void OnCommunicationSuccess( string _ServerAnswer );
    public delegate void OnCommunicationError( string _ErrorMsg );

    public delegate void OnLoadReferentsSuccess( List<Referent> _Result );
    public delegate void OnLoadEventsSuccess( List<Event> _Result);
    public delegate void OnLoadNotificationsSuccess(List<Notification> _Result);

    public delegate void OnHandleCheckCustomerKey( string _CustomerKey );

    enum EMessageTypes
    {
        MSG_LOAD_REFERENTS,
        MSG_LOAD_ALL_EVENTS,
        MSG_LOAD_USER_EVENTS,
        MSG_LOAD_EVENT_COUNT,
        MSG_CHECK_UNLOCK_KEY,
        MSG_SEND_RESERVATION,
        MSG_SEND_USER_DATA,
        MSG_LOAD_NOTIFICATIONS,
        MSG_SEND_FEEDBACK,
        MSG_LOAD_BREAKOUT_SESSION
    }
    // This is totally fake until now
    public class CommunicationService
    {
        private static HttpClient _Client;
        private async static void CallServer( EMessageTypes _MsgType , string _Data , Delegate _OnSuccess)
        {
            if ( _Client == null )
            {
                _Client = new HttpClient();
                _Client.BaseAddress = new Uri("http://"+Constants.SERVER_URL+'/');
                _Client.DefaultRequestHeaders.Accept.Clear();
                _Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                _Client.Timeout = TimeSpan.FromSeconds(30);
            }

            try
            {
                HttpResponseMessage HResponse = null;
                string HSubUrl = GetSubUrl(_MsgType);
                if (IsGetCall(_MsgType))
                {
                    HResponse = _Client.GetAsync(HSubUrl).Result;
                }
                else
                {
                    var HPostContent = new StringContent(_Data, Encoding.UTF8, "application/json");
                    HResponse = _Client.PostAsync(HSubUrl, HPostContent).Result;
                }

                string HContent = await HResponse.Content.ReadAsStringAsync();
                HandleServerResponse(HContent, _MsgType, _OnSuccess);
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch ( Exception _Exception)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                HandleServerResponse("error", _MsgType, _OnSuccess);
                //HandleException(Messages.EXCEPTION_TIMEOUT);
            }
        }

        private static bool IsGetCall( EMessageTypes _MsgType )
        {
            switch ( _MsgType )
            {
                case EMessageTypes.MSG_LOAD_REFERENTS:
                    return true;
                case EMessageTypes.MSG_LOAD_ALL_EVENTS:
                    return false;
                case EMessageTypes.MSG_SEND_USER_DATA:
                    return false;
                case EMessageTypes.MSG_CHECK_UNLOCK_KEY:
                    return false;
                case EMessageTypes.MSG_SEND_RESERVATION:
                    return false;
                case EMessageTypes.MSG_LOAD_USER_EVENTS:
                    return false;
                case EMessageTypes.MSG_LOAD_NOTIFICATIONS:
                    return false;
                case EMessageTypes.MSG_SEND_FEEDBACK:
                    return false;
                case EMessageTypes.MSG_LOAD_BREAKOUT_SESSION:
                    return false;

                default:
                    // WTF??
                    throw new NotImplementedException();
            }
        }

        private static string GetSubUrl( EMessageTypes _MsgType )
        {
            switch ( _MsgType )
            {
                case EMessageTypes.MSG_LOAD_REFERENTS:
                    return "referents/list";
                case EMessageTypes.MSG_LOAD_ALL_EVENTS:
                    return "events/list";
                case EMessageTypes.MSG_SEND_USER_DATA:
                    return "users/add";
                case EMessageTypes.MSG_CHECK_UNLOCK_KEY:
                    return "companies/check";
                case EMessageTypes.MSG_SEND_RESERVATION:
                    return "events/reserve";
                case EMessageTypes.MSG_LOAD_USER_EVENTS:
                    return "events/listuserevents";
                case EMessageTypes.MSG_LOAD_NOTIFICATIONS:
                    return "notifications/list";
                case EMessageTypes.MSG_SEND_FEEDBACK:
                    return "feedback/add";
                case EMessageTypes.MSG_LOAD_BREAKOUT_SESSION:
                    return "events/listbreakout";
                default:
                    // WTF??
                    throw new NotImplementedException();
            }
        }
        private static void HandleServerResponse( string _Answer , EMessageTypes _MsgType , Delegate _OnSuccess )
        {
            if (_Answer == "error" )
            {
                // Silent Handling of Error Answer
                switch ( _MsgType )
                {
                    case EMessageTypes.MSG_SEND_USER_DATA:
                        FileService.SetStorageEntry(Constants.STORAGE_KEY_USER_NOT_SEND,FileService.GetUserData());
                        
                        break;
                }
                
            }
            switch ( _MsgType )
            {
                case EMessageTypes.MSG_LOAD_REFERENTS:
                    List<Referent> HReferents = new List<Referent>();
                    HReferents = JsonConvert.DeserializeObject<List<Referent>>(_Answer);
                    Device.BeginInvokeOnMainThread(() => (_OnSuccess as OnLoadReferentsSuccess)(HReferents));
                    break;
                case EMessageTypes.MSG_LOAD_ALL_EVENTS:
                    List<Event> HEvents = new List<Event>();
                    HEvents = JsonConvert.DeserializeObject<List<Event>>(_Answer);
                    HEvents.Sort(delegate(Event _A, Event _B) {
                        return _A.START_AS_DATE.CompareTo(_B.START_AS_DATE);
                    });
                    foreach ( Event HEvent in HEvents )
                    {
                        // Referents setzen
                        HEvent.GenerateReferent();
                        HEvent.IsInbound = (HEvents.IndexOf(HEvent) % 2) == 0;
                    }
                    Device.BeginInvokeOnMainThread(() => (_OnSuccess as OnLoadEventsSuccess)(HEvents));
                    break;
                case EMessageTypes.MSG_LOAD_BREAKOUT_SESSION:
                    List<Event> HBreakoutEvents = new List<Event>();
                    HBreakoutEvents = JsonConvert.DeserializeObject<List<Event>>(_Answer);
                    foreach (Event HEvent in HBreakoutEvents)
                    {
                        // Referents setzen
                        HEvent.GenerateReferent();
                    }
                    Device.BeginInvokeOnMainThread(() => (_OnSuccess as OnLoadEventsSuccess)(HBreakoutEvents));
                    break;
                case EMessageTypes.MSG_LOAD_USER_EVENTS:
                    List<Event> HPersonalEvents = new List<Event>();
                    HPersonalEvents = JsonConvert.DeserializeObject<List<Event>>(_Answer);
                    HPersonalEvents.Sort(delegate (Event _A, Event _B) {
                        return _A.START_AS_DATE.CompareTo(_B.START_AS_DATE);
                    });
                    foreach (Event HPersonalEvent in HPersonalEvents)
                    {
                        // Referents setzen
                        if (HPersonalEvent.CAN_BE_RESERVED)
                        {
                            HPersonalEvent.USER_HAS_RESERVED = true;
                        }
                        
                        HPersonalEvent.GenerateReferent();
                        HPersonalEvent.IsInbound = (HPersonalEvents.IndexOf(HPersonalEvent) % 2) == 0;
                    }
                    Device.BeginInvokeOnMainThread(() => (_OnSuccess as OnLoadEventsSuccess)(HPersonalEvents));
                    break;
                case EMessageTypes.MSG_SEND_USER_DATA:
                    if ( FileService .DoesStorageEntryExist ( Constants.STORAGE_KEY_USER_NOT_SEND ) )
                    {
                        FileService.RemoveStorageEntry(Constants.STORAGE_KEY_USER_NOT_SEND);
                    }
                    break;
                case EMessageTypes.MSG_CHECK_UNLOCK_KEY:
                    Device.BeginInvokeOnMainThread(() => (_OnSuccess as OnHandleCheckCustomerKey)(_Answer));
                    break;
                case EMessageTypes.MSG_SEND_RESERVATION:
                    Device.BeginInvokeOnMainThread(() => (_OnSuccess as OnCommunicationSuccess)(_Answer));
                    break;
                case EMessageTypes.MSG_LOAD_NOTIFICATIONS:
                    List<Notification> HNotifications = new List<Notification>();
                    HNotifications = JsonConvert.DeserializeObject<List<Notification>>(_Answer);
                    
                    Device.BeginInvokeOnMainThread(() => (_OnSuccess as OnLoadNotificationsSuccess)(HNotifications));
                    break;
                case EMessageTypes.MSG_SEND_FEEDBACK:
                    Device.BeginInvokeOnMainThread(() => (_OnSuccess as OnCommunicationSuccess)(_Answer));
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        public static async Task LoadReferents(OnLoadReferentsSuccess _OnSuccess )
        {  
            CallServer(EMessageTypes.MSG_LOAD_REFERENTS, "", _OnSuccess);  
        }
        public static async Task LoadTimeline(OnLoadEventsSuccess _OnSuccess )
        {
            string HData = JsonConvert.SerializeObject(new PersonalTimelineObject { IDENT = FileService.GetStorageValue(Constants.STORAGE_KEY_USER).ToString() });
            CallServer(EMessageTypes.MSG_LOAD_ALL_EVENTS, HData, _OnSuccess);
        }
        public static async Task LoadBreakoutSession(string _DateTime, OnLoadEventsSuccess _OnSuccess)
        {
            string HData = JsonConvert.SerializeObject(new PersonalTimelineObject { START_DATE = _DateTime , IDENT = FileService.GetStorageValue(Constants.STORAGE_KEY_USER).ToString() });
            CallServer(EMessageTypes.MSG_LOAD_BREAKOUT_SESSION, HData, _OnSuccess);
        }
        public static async Task LoadPersonalTimeline(OnLoadEventsSuccess _OnSuccess)
        {
            string HData = JsonConvert.SerializeObject(new PersonalTimelineObject { IDENT = FileService.GetStorageValue(Constants.STORAGE_KEY_USER).ToString() });
            CallServer(EMessageTypes.MSG_LOAD_USER_EVENTS, HData, _OnSuccess);
        }
        public static async Task SaveUser( User _User )
        {
            string HSerializedUser = JsonConvert.SerializeObject(_User);
            CallServer(EMessageTypes.MSG_SEND_USER_DATA,HSerializedUser,null);
        }
        public static async Task CheckUnlockKey(string _UnlockKey, OnHandleCheckCustomerKey _OnSuccess)
        {
            string HData = JsonConvert.SerializeObject(new UnlockObject(_UnlockKey));
            CallServer(EMessageTypes.MSG_CHECK_UNLOCK_KEY, HData, _OnSuccess);
        }

        public static async Task ReserveEvent( int _Ident , OnCommunicationSuccess _OnSuccess )
        {
            string HData = JsonConvert.SerializeObject(new ReserveObject(_Ident,FileService.GetStorageValue(Constants.STORAGE_KEY_USER).ToString()));
            CallServer(EMessageTypes.MSG_SEND_RESERVATION, HData, _OnSuccess);
        }
        public static async Task SendFeedback( int _Rating, int _Event, OnCommunicationSuccess _OnSuccess)
        {
            string HData = JsonConvert.SerializeObject(new FeedbackObject() { RATING = _Rating, EVENT_IDENT=_Event, USER_IDENT = FileService.GetStorageValue(Constants.STORAGE_KEY_USER).ToString() });
            CallServer(EMessageTypes.MSG_SEND_FEEDBACK,HData,_OnSuccess);
        }

        public static async Task LoadNotifications( string _LastLoaded, OnLoadNotificationsSuccess _OnSuccess)
        {
            string HData = JsonConvert.SerializeObject(new PersonalTimelineObject { IDENT = FileService.GetStorageValue(Constants.STORAGE_KEY_USER).ToString(), LAST_EDITED = _LastLoaded });
            CallServer(EMessageTypes.MSG_LOAD_NOTIFICATIONS, HData, _OnSuccess);
        }

        public static void CancelAllRequests()
        {
            _Client.CancelPendingRequests();
        }

        private static void HandleException( string _ErrorMsg , bool _InBackground = false )
        {
            if ( !_InBackground )
            {
                // TODO GO BACK TO HOMESCREEN
                Device.BeginInvokeOnMainThread(() => NotificationService.ShowAlert(Messages.ERROR, _ErrorMsg, Messages.CLOSE));
            }
            
        }
    }
}
