using Microsoft.Rest;
using Microsoft.Rest.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CustomTranslator
{
    public partial class MicrosoftCustomTranslatorAPIPreview10 : ServiceClient<MicrosoftCustomTranslatorAPIPreview10>, IMicrosoftCustomTranslatorAPIPreview10
    {
        /// <summary>
        /// Gets the authorization token.
        /// </summary>
        /// <param name='apikey'>
        /// Account key
        /// </param>
        /// <param name='region'>
        /// Region for the key
        /// </param>
        /// <param name='customHeaders'>
        /// Headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        /// <exception cref="HttpOperationException">
        /// Thrown when the operation returned an invalid status code
        /// </exception>
        /// <exception cref="SerializationException">
        /// Thrown when unable to deserialize the response
        /// </exception>
        /// <exception cref="ValidationException">
        /// Thrown when a required parameter is null
        /// </exception>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown when a required parameter is null
        /// </exception>
        /// <return>
        /// A response object containing the response body and response headers.
        /// </return>
        public async Task<HttpOperationResponse<string>> AuthTokenGetWithHttpMessagesAsync(string apikey, string region, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken))
        {

            // Tracing
            bool _shouldTrace = ServiceClientTracing.IsEnabled;
            string _invocationId = null;
            if (_shouldTrace)
            {
                _invocationId = ServiceClientTracing.NextInvocationId.ToString();
                Dictionary<string, object> tracingParameters = new Dictionary<string, object>();
                tracingParameters.Add("cancellationToken", cancellationToken);
                ServiceClientTracing.Enter(_invocationId, this, "GetAuthToken", tracingParameters);
            }
            // Construct URL
            string _url = string.Empty;
            if (region.ToLower() == "global")
            {
                _url = "https://api.cognitive.microsoft.com/sts/v1.0/issueToken";
            }
            else
            {
                _url = $"https://{region}.api.cognitive.microsoft.com/sts/v1.0/issueToken";
            }
            // Create HTTP transport objects
            var _httpRequest = new HttpRequestMessage();
            HttpResponseMessage _httpResponse = null;
            _httpRequest.Method = new HttpMethod("POST");
            _httpRequest.RequestUri = new System.Uri(_url);
            // Set Headers
            _httpRequest.Headers.Add("Ocp-Apim-Subscription-Key", apikey);

            if (customHeaders != null)
            {
               foreach (var _header in customHeaders)
               {
                   if (_httpRequest.Headers.Contains(_header.Key))
                   {
                       _httpRequest.Headers.Remove(_header.Key);
                   }
                   _httpRequest.Headers.TryAddWithoutValidation(_header.Key, _header.Value);
               }
            }

            // Serialize Request
            string _requestContent = null;
            // Send Request
            if (_shouldTrace)
            {
                ServiceClientTracing.SendRequest(_invocationId, _httpRequest);
            }
            cancellationToken.ThrowIfCancellationRequested();
            System.Diagnostics.Debug.WriteLine("Sending request");
            _httpResponse = await HttpClient.SendAsync(_httpRequest, cancellationToken).ConfigureAwait(false);
            if (_shouldTrace)
            {
                ServiceClientTracing.ReceiveResponse(_invocationId, _httpResponse);
            }
            HttpStatusCode _statusCode = _httpResponse.StatusCode;
            System.Diagnostics.Debug.WriteLine($"Response: {_statusCode}");
            cancellationToken.ThrowIfCancellationRequested();
            string _responseContent = null;
            if ((int)_statusCode != 200)
            {
                var ex = new HttpOperationException(string.Format("Operation returned an invalid status code '{0}'", _statusCode));
                if (_httpResponse.Content != null)
                {
                    _responseContent = await _httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                }
                else
                {
                    _responseContent = string.Empty;
                }
                ex.Request = new HttpRequestMessageWrapper(_httpRequest, _requestContent);
                ex.Response = new HttpResponseMessageWrapper(_httpResponse, _responseContent);
                if (_shouldTrace)
                {
                    ServiceClientTracing.Error(_invocationId, ex);
                }
                _httpRequest.Dispose();
                if (_httpResponse != null)
                {
                    _httpResponse.Dispose();
                }
                throw ex;
            }
            // Create Result
            var _result = new HttpOperationResponse<string>();
            _result.Request = _httpRequest;
            _result.Response = _httpResponse;
            // Deserialize Response
            if ((int)_statusCode == 200)
            {
                _responseContent = await _httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                _result.Body = _responseContent;
            }
            if (_shouldTrace)
            {
                ServiceClientTracing.Exit(_invocationId, _result);
            }
            return _result;
        }
    }
}
