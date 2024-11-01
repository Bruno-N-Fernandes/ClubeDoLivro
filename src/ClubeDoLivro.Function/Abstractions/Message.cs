using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.Json;

namespace ClubeDoLivro.Function.Abstractions
{
	public class Message
    {
        [JsonProperty("messages")]
        public List<string> Messages { get; }

        public Message() => Messages = [];

		public Message(string message) => Messages = [message];

        public Message(IEnumerable<string> message) => Messages = new List<string>(message);
    }
}