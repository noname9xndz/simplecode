﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolicyService.Domain
{
    public interface IOfferRepository
    {
        void Add(Offer offer);

        Task<Offer> WithNumber(string number);
    }
}
