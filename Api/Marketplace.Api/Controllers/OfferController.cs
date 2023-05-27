// <copyright company="ROSEN Swiss AG">
//  Copyright (c) ROSEN Swiss AG
//  This computer program includes confidential, proprietary
//  information and is a trade secret of ROSEN. All use,
//  disclosure, or reproduction is prohibited unless authorized in
//  writing by an officer of ROSEN. All Rights Reserved.
// </copyright>
namespace Marketplace.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Marketplace.Core.Bl;
    using Marketplace.Core.Model;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Services for Offers.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [ApiController]
    [Route("[controller]")]
    public class OfferController : ControllerBase
    {
        #region Fields

        private readonly ILogger<OfferController> logger;
        private readonly IOfferBl offerBl;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="OfferController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="offerBl">The offer business logic.</param>
        public OfferController(ILogger<OfferController> logger, IOfferBl offerBl)
        {
            this.logger = logger;
            this.offerBl = offerBl;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the list of offers.
        /// </summary>
        /// <param name="pageIndex">The page index.</param>
        /// <returns>A list of offers.</returns>
        [HttpGet]
        public async Task<ActionResult<Page<Offer>>> Get(int pageIndex = 0)
        {
            IEnumerable<Offer> offerResult;
            Page<Offer> offerPage = new Page<Offer>(new List<Offer>(), 0, 0);

            try
            {
                offerResult = await this.offerBl.GetOffersAsync(pageIndex).ConfigureAwait(false);
                offerPage.Items = offerResult.ToList();
            }
            catch (Exception ex)
            {
                this.logger?.LogError(ex, ex.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Server Error.");
            }

            return this.Ok(offerPage);
        }

        /// <summary>
        /// Creates a new offer.
        /// </summary>
        /// <param name="offer">The offer to create.</param>
        /// <returns>The ID of the created offer.</returns>
        [HttpPost]
        public async Task<ActionResult<Guid>> CreateOffer(Offer offer)
        {
            try
            {
                var offerId = await offerBl.CreateOfferAsync(offer).ConfigureAwait(false);
                return offerId;
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Gets the count of offers.
        /// </summary>
        /// <returns>The count of offers.</returns>
        [HttpGet("count")]
        public async Task<ActionResult<int>> GetOffersCount()
        {
            try
            {
                int offerCount = await offerBl.GetOffersCountAsync().ConfigureAwait(false);
                return offerCount;
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        #endregion
    }
}
