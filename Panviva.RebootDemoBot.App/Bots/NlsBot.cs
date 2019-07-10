// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with Bot Builder V4 SDK Template for Visual Studio EchoBot v4.3.0

using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Panviva.RebootDemoBot.App.Model;

namespace Panviva.RebootDemoBot.App.Bots
{
    public class NlsBot : ActivityHandler
    {
        private readonly IHttpClientFactory _clientFactory;

        public NlsBot(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["simpleQuery"] = turnContext.Activity.Text;
            query["channel"] = "Chatbot";

            var panvivaClient = _clientFactory.CreateClient("panviva");
            var response = await panvivaClient.GetAsync($"operations/artefact/nls?{query}", cancellationToken);
            var artefactList = JsonConvert.DeserializeObject<Artefacts>(await response.Content.ReadAsStringAsync());

            var reply = artefactList?.Results.FirstOrDefault()?.Content.FirstOrDefault()?.Text;

            await turnContext
                .SendActivityAsync(MessageFactory.Text(reply ?? "Sorry I couldn't find anything related to your question."), cancellationToken);
        }
    }
}
