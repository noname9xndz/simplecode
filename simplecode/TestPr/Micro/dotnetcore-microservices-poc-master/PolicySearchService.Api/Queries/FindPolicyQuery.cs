﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace PolicySearchService.Api.Queries
{
    public class FindPolicyQuery : IRequest<FindPolicyResult>
    {
        public string QueryText { get; set; }
    }
}
