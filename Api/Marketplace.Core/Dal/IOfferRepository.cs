// <copyright company="ROSEN Swiss AG">
//  Copyright (c) ROSEN Swiss AG
//  This computer program includes confidential, proprietary
//  information and is a trade secret of ROSEN. All use,
//  disclosure, or reproduction is prohibited unless authorized in
//  writing by an officer of ROSEN. All Rights Reserved.
// </copyright>

using System;
using System.Threading.Tasks;
using Marketplace.Core.Model;

namespace Marketplace.Core.Dal;

/// <summary>
///     Contract for the User data access
/// </summary>
public interface IOfferRepository
{
    #region Methods

    /// <summary>
    ///     Gets all offers asynchronously.
    /// </summary>
    /// <param name="pageIndex">The page index.</param>
    /// <returns>Array of offers</returns>
    Task<Offer[]> GetPaginatedOffersAsync(int pageIndex);

    Task<Guid> CreateOfferAsync(Offer offer);

    Task<int> GetOffersCountAsync();


    #endregion
}