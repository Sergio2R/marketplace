import { Component, OnInit, ViewChild } from '@angular/core';
import { MarketplaceApiService } from 'src/app/core/marketplace-api/marketplace-api.service';
import { Offer } from 'src/app/core/marketplace-api/models/offer.model';
import { Page } from 'src/app/core/models/page.model';
import { Category } from 'src/app/core/marketplace-api/models/category.model';
@Component({
  selector: 'app-offer-list',
  templateUrl: './offer-list.component.html',
  styleUrls: ['./offer-list.component.scss']
})
export class OfferListComponent implements OnInit {
  inputValue: number; // Declare the inputValue property

  categories: Category[]
  offersPage: Page<Offer>; // Property to store the fetched offers
  actualPage = 0;
  count: number;
  buttons: any;
  totalPages: number;

  constructor(private marketplaceApiService: MarketplaceApiService) {
  }


  ngOnInit() {
    this.getOffersCount();
    this.getCategories();
    this.loadOffersPage(0); // Load the first page initially
  }

  getCategories() {
    this.marketplaceApiService.getCategories().subscribe(categories => {
      this.categories = categories;
    });
  }

  loadOffersPage(pageIndex: number) {
    this.actualPage = pageIndex;
    this.marketplaceApiService.getOffers(pageIndex).subscribe(async page => {
      // Handle the response and update the offers property 
      this.offersPage = page;
      this.assignCategories();
    });
    window.scrollTo({ top: 0, behavior: 'smooth' });
    this.drawPageButtons();
  }

  assignCategories() {
    this.offersPage.items.forEach((offer) => {
      const category = this.categories.find((cat) => cat.id === offer.categoryId);
      if (category) {
        offer.categoryName = category.name;
      }
    });
  }

  goToPage() {
    this.loadOffersPage(this.actualPage);
    window.scrollTo({ top: 0, behavior: 'smooth' });
  }

  getOffersCount() {
    this.marketplaceApiService.getOffersCount().subscribe(
      count => {
        this.count = count;
        this.drawPageButtons();
      },
      error => {
        console.error('Error getting offers count:', error);
      }
    );
  }

  drawPageButtons() {
    this.totalPages = Math.ceil(this.count / 10); // Calculate the total number of pages
    this.buttons = [];
    let startPage = Math.max(0, this.actualPage - 3);
    let endPage = Math.min(this.totalPages - 1, this.actualPage + 3);
    if (startPage > 0) {
      this.buttons.push({ label: '<<', pageIndex: this.actualPage - 1 });
    }
    for (let i = startPage; i <= endPage; i++) {
      this.buttons.push({ label: String(i + 1), pageIndex: i });
    }
    if (endPage < this.totalPages - 1) {
      this.buttons.push({ label: '>>', pageIndex: this.actualPage + 1 });
    }
  }


}
