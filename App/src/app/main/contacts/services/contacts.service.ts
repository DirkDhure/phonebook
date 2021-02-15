import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Subject } from 'rxjs';


const baseUrl = '';

@Injectable({
  providedIn: 'root'
})
export class ContactsService {
  contactSelected: BehaviorSubject<any>;
  groupSelected: BehaviorSubject<any>;
  selected: BehaviorSubject<any>;

  onSearchTextChanged: Subject<any>;

  loadPhoneBook(email: string): any {
    return this._httpClient.get(baseUrl + '/api/phone-books/' + email);
  }

  constructor(private _httpClient: HttpClient) {
    this.contactSelected = new BehaviorSubject(null);
    this.groupSelected = new BehaviorSubject(null);
    this.selected = new BehaviorSubject([]);
    this.onSearchTextChanged = new Subject();
   }
}
