import { Component, OnInit } from '@angular/core';
import { MarketplaceApiService } from 'src/app/core/marketplace-api/marketplace-api.service';
import { Category } from 'src/app/core/marketplace-api/models/category.model';
import { Offer } from 'src/app/core/marketplace-api/models/offer.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-offer-creation',
  templateUrl: './offer-creation.component.html',
  styleUrls: ['./offer-creation.component.scss']
})
export class OfferCreationComponent implements OnInit {
  offer: Offer = new Offer('');
  categories: Category[]


  constructor(
    private router: Router,
    private marketplaceApiService: MarketplaceApiService,
    ) { }

  ngOnInit(): void {
    if (!localStorage.getItem("userid")) {
      this.router.navigate(['/Login']);
    }
    this.getCategories();
  }

  getCategories() {
    this.marketplaceApiService.getCategories().subscribe(categories => {
      this.categories = categories;
    });
  }

  onSubmit(): void {
    this.marketplaceApiService.createOffer(this.offer);
  }
}



