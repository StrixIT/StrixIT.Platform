//-----------------------------------------------------------------------
// <copyright file="IHttpService.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace StrixIT.Platform.Web
{
    public interface IHttpService
    {
        void CompressRequest();
        void SetResponseHeaders();
        void SetCultureForRequest();
    }
}
