// <copyright company="ROSEN Swiss AG">
//  Copyright (c) ROSEN Swiss AG
//  This computer program includes confidential, proprietary
//  information and is a trade secret of ROSEN. All use,
//  disclosure, or reproduction is prohibited unless authorized in
//  writing by an officer of ROSEN. All Rights Reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Marketplace.Core.Model;

namespace Marketplace.Core.Bl;

/// <summary>
///     Contract for the User logic
/// </summary>
public interface IOfferBl
{
    #region Methods


    /// <summary>
    ///     Gets the offers.
    /// </summary>
    /// <param name="pageIndex">The page index.</param>
    /// <returns>List of offers</returns>
    Task<IEnumerable<Offer>> GetOffersAsync(int pageIndex);

    Task<Guid> CreateOfferAsync(Offer offer);

    Task<int> GetOffersCountAsync();

    #endregion
}