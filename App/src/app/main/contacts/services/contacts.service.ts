import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';
import { BehaviorSubject, Subject } from 'rxjs';
import { BookDetailsModel } from '../models/book-detail.model';
import { ContactEntryModel } from '../models/contact-entry.model';


const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
}

@Injectable({
  providedIn: 'root'
})
export class ContactsService {

  contactSelected: BehaviorSubject<any>;
  groupSelected: BehaviorSubject<any>;
  selected: BehaviorSubject<any>;
  phoneBookUrl = "https://localhost:44353";
  onSearchTextChanged: Subject<any>;
  phoneBook: BookDetailsModel;
  value: any;

  loadPhoneBook(email: string): any {
    return this._httpClient.get(this.phoneBookUrl + '/api/phone-books/' + email);
  }
  addContact(contact: ContactEntryModel): any {
    return this._httpClient.post(this.phoneBookUrl + '/api/phone-books/' + this.phoneBook.id, contact, httpOptions);
  }
  updateContact(contact: Partial<ContactEntryModel>): any {
    return this._httpClient.put(this.phoneBookUrl + '/api/phone-books/' + this.phoneBook.id, contact, httpOptions);
  }
  constructor(private _httpClient: HttpClient) {
    this.contactSelected = new BehaviorSubject(null);
    this.groupSelected = new BehaviorSubject(null);
    this.selected = new BehaviorSubject([]);
    this.onSearchTextChanged = new Subject();

  }
}
