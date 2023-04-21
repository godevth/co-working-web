using CoreCMS.Application.Place.Models;
using CoreCMS.Application.Shared.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreCMS.Application.Place.Commands
{
   public class EditPlaceCommand : PlaceModels , IRequest<CommandResult<bool>>
    {
        public string UpdateUserId { get; set; }
    }
}
