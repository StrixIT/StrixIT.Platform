//-----------------------------------------------------------------------
// <copyright file="ConfirmationConfig.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace StrixIT.Platform.Web
{
    public class ConfirmationConfig
    {
        public ConfirmationConfig() : this(null) { }

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
