using System;
using System.ComponentModel.DataAnnotations;
using CoreCMS.Application.Shared.Model;
using MediatR;

namespace CoreCMS.Application.User.Queries
{
    public class Select2UserPrivilegeQuery : IRequest<OptionViewModel[]>
    {
        public string Search { get; set; }

        [Required]
        public Guid PlaceId { get; set; }
    }
}