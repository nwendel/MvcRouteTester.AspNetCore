﻿#region License
// Copyright (c) Niklas Wendel 2018
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

namespace MvcRouteTester.AspNetCore.Internal
{

    /// <summary>
    /// 
    /// </summary>
    public class TypeNameInfo
    {

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        public TypeNameInfo(Type type)
        {
            if(type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            Name = type.Name;
            FullName = type.FullName;
            AssemblyQualifiedName = type.AssemblyQualifiedName;
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 
        /// </summary>
        public string FullName { get; }

        /// <summary>
        /// 
        /// </summary>
        public string AssemblyQualifiedName { get; }

        #endregion

    }

}
