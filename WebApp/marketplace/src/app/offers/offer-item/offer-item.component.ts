import { Component, Input, OnInit } from '@angular/core';
import { Offer } from '../../core/marketplace-api/models/offer.model';

@Component({
  selector: 'app-offer-item',
  templateUrl: './offer-item.component.html',
  styleUrls: ['./offer-item.component.scss']
})
export class OfferItemComponent implements OnInit {

  @Input()
  offer: Offer;
  host: string = 'http://localhost:4200/assets/';

  constructor() { }

  ngOnInit(): void {
  }

  //I wanted to include random images but got caught validating the origin of the file, 
  //so i was forced to leave it like this. A validation of the original lenght
  generateImageUrl(): string {
    if (this.offer.pictureUrl.length < 17)
      return this.host + this.offer.pictureUrl;
    else return this.offer.pictureUrl;
  }

}
