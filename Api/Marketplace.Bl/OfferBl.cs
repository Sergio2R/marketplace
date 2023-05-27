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
using Marketplace.Core.Bl;
using Marketplace.Core.Dal;
using Marketplace.Core.Model;

namespace Marketplace.Bl
{
    /// <summary>
    ///     Offer logic
    /// </summary>
    /// <seealso cref="Marketplace.Core.Bl.IOfferBl" />
    public class OfferBl : IOfferBl
    {
        #region Fields

        private readonly IOfferRepository offerRepository;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="OfferBl"/> class.
        /// </summary>
        /// <param name="offerRepository">The offer repository.</param>
        public OfferBl(IOfferRepository offerRepository)
        {
            this.offerRepository = offerRepository;
        }

        #endregion

        #region Methods

        /// <inheritdoc />
        public async Task<IEnumerable<Offer>> GetOffersAsync(int pageIndex)
        {
            return await offerRepository.GetPaginatedOffersAsync(pageIndex).ConfigureAwait(false);
        }

        public async Task<Guid> CreateOfferAsync(Offer offer)
        {
            return await offerRepository.CreateOfferAsync(offer).ConfigureAwait(false);
        }

        public async Task<int> GetOffersCountAsync()
        {
            return await offerRepository.GetOffersCountAsync().ConfigureAwait(false);
        }

        #endregion
    }
}
