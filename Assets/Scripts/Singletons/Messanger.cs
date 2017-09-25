// Usage example:
// Messenger.AddListener<float>("myEvent", MyEventHandler);
// ...
// Messenger.Broadcast<float>("myEvent", 1.0f);


using System;
using System.Collections.Generic;

static internal class MessengerInternal {
    static private Dictionary<string, Delegate> eventTable = new Dictionary<string, Delegate>();

    static public void AddListener(string eventType, Delegate handler) {
        if (!eventTable.ContainsKey(eventType)) {
            eventTable.Add(eventType, null);
        }

        Delegate d = eventTable[eventType];
        if(d != null && d.GetType() != handler.GetType()) {
            throw new ListenerException(string.Format("Attempting to add listener with inconsistent signature for event type {0}. Current listeners have type {1} and listener being added has type {2}", eventType, d.GetType().Name, handler.GetType().Name));
        }
        eventTable[eventType] = Delegate.Combine(d, handler);
    }

    static public void RemoveListener(string eventType, Delegate handler) {
        if (eventTable.ContainsKey(eventType)) {
            Delegate d = eventTable[eventType];

            if (d == null) {
                throw new ListenerException(string.Format("Attempting to remove listener with for event type {0} but current listener is null.", eventType));
            } else if (d.GetType() != handler.GetType()) {
                throw new ListenerException(string.Format("Attempting to remove listener with inconsistent signature for event type {0}. Current listeners have type {1} and listener being removed has type {2}", eventType, d.GetType().Name, handler.GetType().Name));
            }

            eventTable[eventType] = Delegate.Remove(d, handler);

            if(eventTable[eventType] == null) {
                eventTable.Remove(eventType);
            }
        } else {
            throw new ListenerException(string.Format("Attempting to remove listener for type {0} but Messenger doesn't know about this event type.", eventType));
        }
    }

    static public Delegate GetCallback(string eventType) {
        Delegate d;
        if(eventTable.TryGetValue(eventType, out d))
            return d;

        return null;
    }

    static public BroadcastException CreateBroadcastSignatureException(string eventType) {
        return new BroadcastException(string.Format("Broadcasting message {0} but listeners have a different signature than the broadcaster.", eventType));
    }

    public class BroadcastException : Exception {
        public BroadcastException(string msg)
            : base(msg) {
        }
    }

    public class ListenerException : Exception {
        public ListenerException(string msg)
            : base(msg) {
        }
    }
}


// No parameters
static public class Messenger {

    static public void AddListener(string eventType, Callback handler) {
        MessengerInternal.AddListener(eventType, handler);
    }

    static public void AddListener<T>(string eventType, Callback<T> handler) {
        MessengerInternal.AddListener(eventType, handler);
    }

    static public void AddListener<T, U>(string eventType, Callback<T, U> handler) {
        MessengerInternal.AddListener(eventType, handler);
    }

    static public void RemoveListener(string eventType, Callback handler) {
        MessengerInternal.RemoveListener(eventType, handler);  
    }
    
    static public void RemoveListener<T>(string eventType, Callback<T> handler) {
        MessengerInternal.RemoveListener(eventType, handler);
    }

    static public void RemoveListener<T, U>(string eventType, Callback<T, U> handler) {
        MessengerInternal.RemoveListener(eventType, handler);
    }

    static public void Broadcast(string eventType) {
        Delegate d = MessengerInternal.GetCallback(eventType);
        if(d != null) {
            Callback callback = d as Callback;
            if(callback != null) {
                callback();
            } else {
                throw MessengerInternal.CreateBroadcastSignatureException(eventType);
            }
        }
    }

    static public void Broadcast<T>(string eventType, T arg1) {
        Delegate d = MessengerInternal.GetCallback(eventType);
        if(d != null) {
            Callback<T> callback = d as Callback<T>;
            if(callback != null) {
                callback(arg1);
            } else {
                throw MessengerInternal.CreateBroadcastSignatureException(eventType);
            }
        }
    }

    static public void Broadcast<T, U>(string eventType, T arg1, U arg2) {
        Delegate d = MessengerInternal.GetCallback(eventType);
        if(d != null) {
            Callback<T, U> callback = d as Callback<T, U>;
            if(callback != null) {
                callback(arg1, arg2);
            } else {
                throw MessengerInternal.CreateBroadcastSignatureException(eventType);
            }
        }
    }
}
