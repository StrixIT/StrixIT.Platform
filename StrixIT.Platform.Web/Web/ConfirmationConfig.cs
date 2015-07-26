#region Apache License

//-----------------------------------------------------------------------
// <copyright file="ConfirmationConfig.cs" company="StrixIT">
// Copyright 2015 StrixIT. Author R.G. Schurgers MA MSc.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
//-----------------------------------------------------------------------

#endregion Apache License

namespace StrixIT.Platform.Web
{
    public class ConfirmationConfig
    {
        public ConfirmationConfig() : this(null)
        {
        }

        public ConfirmationConfig(string id)
        {
            this.Id = string.IsNullOrWhiteSpace(id) ? "confirmationmodal" : id;
            this.Title = Core.Resources.DefaultInterface.DeleteItemTitle;
            this.Body = Core.Resources.DefaultInterface.ConfirmDeleteItem;
            this.ConfirmMethod = "confirmDelete";
        }

        public string Id { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public string ConfirmMethod { get; set; }

        public string ScopeId { get; set; }
    }
}