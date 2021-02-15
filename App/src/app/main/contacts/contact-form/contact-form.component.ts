import { Component, Inject, OnInit, ViewEncapsulation } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, FormArray } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { ContactEntryModel } from '../models/contact-entry.model';

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
  constructor(
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
    return this._formBuilder.group({
      id: [this.contact.id],
      name: [this.contact.name],
      lastName: [this.contact.lastName],
      avatar: [this.contact.avatar],
      nickname: [this.contact.nickname],
      company: [this.contact.companyName],
      contacts: this._formBuilder.array([this.contact.contacts])
    });
  }
  createItem(): any {
    return this._formBuilder.group({
      contactType: [''],
      text: ['']
    });
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
    this.contact.firstName = this.contactForm.value['firstName'];
    this.contact.lastName = this.contactForm.value['lastName'];
    this.contact.company = this.contactForm.value['companyName'];
    this.contact.contacts = this.contactForm.value['contacts'];
    // this.store.dispatch({})
  }
}
