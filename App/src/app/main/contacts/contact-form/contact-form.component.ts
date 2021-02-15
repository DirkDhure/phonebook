import { Component, Inject, OnInit, ViewEncapsulation } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, FormArray } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { Update } from '@ngrx/entity';
import { Store } from '@ngrx/store';
import { AppState } from 'app/store/reducers';
import * as _ from 'lodash';
import { ContactEntryModel } from '../models/contact-entry.model';
import { addContact, updateContact } from '../store/actions/contacts.actions';

@Component({
  selector: 'app-contact-form',
  templateUrl: './contact-form.component.html',
  styleUrls: ['./contact-form.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class ContactFormComponent implements OnInit {
  action: string;
  contact: any;
  contactForm: FormGroup;
  dialogTitle: string;
  arr: FormArray;

  /**
   * Constructor
   *
   * @param {MatDialogRef<ContactsContactFormDialogComponent>} matDialogRef
   * @param _data
   * @param {FormBuilder} _formBuilder
   */
  constructor(private store: Store<AppState>,
    public matDialogRef: MatDialogRef<ContactFormComponent>,
    @Inject(MAT_DIALOG_DATA) private _data: any,
    private _formBuilder: FormBuilder
  ) {
    // Set the defaults
    this.action = _data.action;

    if (this.action === 'edit') {
      this.dialogTitle = 'Edit Contact';
      this.contact = _data.contact;
    }
    else {
      this.dialogTitle = 'New Contact';
      this.contact = new ContactEntryModel();
    }
    this.contactForm = this.createContactForm();
  }
  ngOnInit(): void {
  }

  // -----------------------------------------------------------------------------------------------------
  // @ Public methods
  // -----------------------------------------------------------------------------------------------------

  /**
   * Create contact form
   *
   * @returns {FormGroup}
   */
  createContactForm(): FormGroup {
    if (!this.contact.contacts) {
      this.contact.contacts = [];
    }
    return this._formBuilder.group({
      id: [this.contact.id],
      name: [this.contact.firstName],
      lastName: [this.contact.lastName],
      avatar: [this.contact.avatar],
      nickname: [this.contact.nickname],
      company: [this.contact.companyName],
      contacts: this.setContacts()
    });
  }
  createItem(): any {
    return this._formBuilder.group({
      contactType: [''],
      text: ['']
    });
  }


  setEntryContact(contact): any {
    return this._formBuilder.group({
      contactType: [contact.contactType],
      text: [contact.text]
    });

  }

  setContacts(): FormArray {
    if (!this.contact.contacts) {
      this.contact.contacts = []
    }
    const controls = this.contact.contacts.map(contact => {
      return this.setEntryContact(contact);
    });
    return new FormArray(controls);
  }
  adContact(): any {
    const add = this.contactForm.get('contacts') as FormArray;
    add.push(this.createItem());
  }

  removeUser(i): any {
    const remove = this.contactForm.get('contacts') as FormArray;
    remove.removeAt(i);
  }
  onSave(): any {
    this.contact = _.clone(this.contact);
    this.contact.firstName = this.contactForm.value['name'];
    this.contact.lastName = this.contactForm.value['lastName'];
    this.contact.company = this.contactForm.value['companyName'];
    this.contact.contacts = this.contactForm.value['contacts'];
    if (!this.contact.id) {
      this.store.dispatch(addContact({ contact: this.contact }))
    } else {
      const update: Update<ContactEntryModel> = {
        id: this.contact.id,
        changes: this.contact
      }
      this.store.dispatch(updateContact({ update }))
    }
    this.contactForm.reset();
    this.matDialogRef.close();
  }
}
