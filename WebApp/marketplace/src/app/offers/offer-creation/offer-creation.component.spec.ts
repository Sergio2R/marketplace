import { ComponentFixture, TestBed } from '@angular/core/testing';
import { OfferCreationComponent } from './offer-creation.component';
import { MarketplaceApiService } from 'src/app/core/marketplace-api/marketplace-api.service';
import { Router } from '@angular/router';
import { of } from 'rxjs';
import { Category } from 'src/app/core/marketplace-api/models/category.model';
import { Offer } from 'src/app/core/marketplace-api/models/offer.model';

class MockMarketplaceApiService {
  getCategories() {
    // Mock implementation
  }

  createOffer(offer: Offer) {
    // Mock implementation
  }
}

class MockRouter {
  navigateByUrl(url: string) {
    // Mock implementation
  }
}

describe('OfferCreationComponent', () => {
  let component: OfferCreationComponent;
  let fixture: ComponentFixture<OfferCreationComponent>;
  let mockMarketplaceApiService: MockMarketplaceApiService;
  let mockRouter: MockRouter;

  beforeEach(async () => {
    mockMarketplaceApiService = new MockMarketplaceApiService();
    mockRouter = new MockRouter();

    await TestBed.configureTestingModule({
      declarations: [OfferCreationComponent],
      providers: [
        { provide: MarketplaceApiService, useValue: mockMarketplaceApiService },
        { provide: Router, useValue: mockRouter }
      ]
    }).compileComponents();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should navigate to login page if userid is not in local storage', () => {
    spyOn(component['router'], 'navigateByUrl');

    localStorage.removeItem('userid');
    component.ngOnInit();

    expect(component['router'].navigateByUrl).toHaveBeenCalledWith('/Login');
  });

  it('should call createOffer on form submission', () => {
    const offer: Offer = new Offer('Test Offer');
    spyOn(mockMarketplaceApiService, 'createOffer');

    component.offer = offer;
    component.onSubmit();

    expect(mockMarketplaceApiService.createOffer).toHaveBeenCalledWith(offer);
  });
});
