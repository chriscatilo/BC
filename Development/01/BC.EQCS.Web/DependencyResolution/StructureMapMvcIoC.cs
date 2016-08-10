// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StructureMapMvcIoC.cs" company="Web Advanced">
// Copyright 2012 Web Advanced (www.webadvanced.com)
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0

// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


using BC.EQCS.Web.Utils;
using StructureMap;

namespace BC.EQCS.Web.DependencyResolution
{
    public static class StructureMapMvcIoC
    {
        public static IContainer Initialize()
        {
                ObjectFactory.Initialize(x =>
                {
                    x.AddRegistry<SecurityRegistryMvc>();
                    x.AddRegistry<BC.EQCS.Web.DependencyResolution.DefaultRegistry>();
                }); 
            
            return ObjectFactory.Container;
        }
    }
}