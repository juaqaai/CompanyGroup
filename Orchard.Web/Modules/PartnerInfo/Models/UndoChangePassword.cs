using System;
using System.Collections.Generic;

namespace Cms.PartnerInfo.Models
{
    /// <summary>
    /// jelszó visszavonás eredmény
    /// </summary>
    public class UndoChangePassword : CompanyGroup.Dto.PartnerModule.UndoChangePassword
    {
        public UndoChangePassword(CompanyGroup.Dto.PartnerModule.UndoChangePassword undoChangePassword, Cms.CommonCore.Models.Visitor visitor)
        {
            this.Message = undoChangePassword.Message;

            this.Succeeded = undoChangePassword.Succeeded;

            this.Visitor = visitor;
        }

        public Cms.CommonCore.Models.Visitor Visitor { get; set; }
    }
}
