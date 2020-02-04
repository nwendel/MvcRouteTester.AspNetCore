﻿#region License
// Copyright (c) Niklas Wendel 2018-2019
// 
// Licensed under the Apache License, Version 2.0 (the "License"); 
// you may not use this file except in compliance with the License. 
// You may obtain a copy of the License at 
// 
// http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software 
// distributed under the License is distributed on an "AS IS" BASIS, 
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
// See the License for the specific language governing permissions and 
// limitations under the License.
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.TestHost;
using MvcRouteTester.AspNetCore.Internal;

namespace MvcRouteTester.AspNetCore.Builders
{

    /// <summary>
    /// 
    /// </summary>
    public class RouteTesterRequest : IRequestBuilder
    {

        #region Fields

        private HttpMethod _method = HttpMethod.Get;
        private string _pathAndQuery = "/";
        private IDictionary<string, string> _formData = new Dictionary<string, string>();
        private object _jsonData = null;

        #endregion

        #region Builder

        /// <summary>
        /// 
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public IRequestBuilder WithMethod(HttpMethod method)
        {
            if (method == null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            _method = method;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pathAndQuery"></param>
        /// <returns></returns>
        public IRequestBuilder WithPathAndQuery(string pathAndQuery)
        {
            if (pathAndQuery == null)
            {
                throw new ArgumentNullException(nameof(pathAndQuery));
            }

            _pathAndQuery = pathAndQuery;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IRequestBuilder WithFormData(IDictionary<string, string> formData)
        {
            if(formData == null)
            {
                throw new ArgumentNullException(nameof(formData));
            }

            _formData = formData;
            return this;
        }

        // <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IRequestBuilder WithJsonData(object jsonData)
        {
            if (jsonData == null)
            {
                throw new ArgumentNullException(nameof(jsonData));
            }

            _jsonData = jsonData;
            return this;
        }

        #endregion

        #region Execute Async

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<HttpResponseMessage> ExecuteAsync(TestServer server)
        {
            var client = server.CreateClient();
            var requestMessage = new HttpRequestMessage(_method, _pathAndQuery);

            // REVIEW: Only with POST method?
            if (_method == HttpMethod.Post)
            {
                if (_formData.Any())
                {
                    requestMessage.Content = new FormUrlEncodedContent(_formData);
                }
                else if (_jsonData != null)
                {
                    requestMessage.Content = _jsonData.ToHttpContent();
                }
            }

            var responseMessage = await client.SendAsync(requestMessage);
            return responseMessage;
        }

        #endregion
        
    }
}
