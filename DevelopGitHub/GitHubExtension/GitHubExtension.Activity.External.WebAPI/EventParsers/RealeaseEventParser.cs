﻿using Newtonsoft.Json.Linq;

namespace GitHubExtension.Activity.External.WebAPI.EventParsers
{
    class RealeaseEventParser : BaseEventParser
    {
        public override JToken ParseEvent(JToken eventToken)
        {
            JToken parsedEvent = InitialiseEventData(eventToken);

            parsedEvent["payload"]["action"] = eventToken["payload"]["action"];
            parsedEvent["payload"]["release"] = eventToken["payload"]["release"];

            return parsedEvent;
        }
    }
}