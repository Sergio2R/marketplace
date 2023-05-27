import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { Offer } from './models/offer.model';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Page } from '../models/page.model';
import { Category } from './models/category.model';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class MarketplaceApiService {

  private readonly marketplaceApUrl = 'https://localhost:44313';

  constructor(
    private http: HttpClient,
    private router: Router,

  ) { }

  async getUserIdByUsername(username: string): Promise<number> {
    const url = `${this.marketplaceApUrl}/User/validate-existence?username=${encodeURIComponent(username)}`;

    try {
      const userExists = await this.http.get<number>(url).toPromise();
      return userExists;
    } catch (error) {
      console.error('An error occurred while validating user existence:', error);
      throw error;
    }
  }

  getOffers(pageIndex: number): Observable<Page<Offer>> {
    const url = `${this.marketplaceApUrl}/Offer?pageIndex=${pageIndex}`;
    return this.http.get<Page<Offer>>(url);
  }

  getCategories(): Observable<Category[]> {
    const url = `${this.marketplaceApUrl}/Category`;
    return this.http.get<Category[]>(url);
  }

  getRandomImage(): string {
    const width = Math.floor(Math.random() * 50) + 100; // Random width between 200 and 1000
    const height = Math.floor(Math.random() * 50) + 100; // Random height between 200 and 1000
    const apiUrl = `https://picsum.photos/${width}/${height}.jpg`;
    return apiUrl;
  }

  createOffer(offer) {
    const url = `${this.marketplaceApUrl}/Offer`;
    const headers = new HttpHeaders({
      'Content-Type': 'application/json'
    });
    const body = {
      "categoryId": offer.categoryId,
      "description": offer.description,
      "location": offer.location,
      "pictureUrl": this.getRandomImage(),
      "publishedOn": this.setDate(),
      "title": offer.title,
      "userId": 1
    };

    this.http.post(url, body, { headers }).subscribe(
      (response) => {
        this.router.navigate(['/Offer/ListItems']);

      },
      (error) => {
        console.error('Error creating offer:', error);
      }
    );
  }

  setDate() {
    const newDate = new Date();
    newDate.setFullYear(newDate.getFullYear() + 5);
    return newDate;
  }

  getOffersCount(): Observable<number> {
    const url = `${this.marketplaceApUrl}/Offer/count`;
    return this.http.get<number>(url);
  }

}
