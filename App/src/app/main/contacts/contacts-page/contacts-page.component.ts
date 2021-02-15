import { Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatDialog } from '@angular/material';
import { fuseAnimations } from '@fuse/animations';
import { FuseSidebarService } from '@fuse/components/sidebar/sidebar.service';
import { select, Store } from '@ngrx/store';
import { AppState } from 'app/store/reducers';
import { Subject } from 'rxjs';
import { ContactFormComponent } from '../contact-form/contact-form.component';
import { ContactsService } from '../services/contacts.service';
import { loadPhoneBook } from '../store/actions/phone-book.actions';
import { selectContacts } from '../store/selectors/contact-entries.selectors';

@Component({
  selector: 'app-contacts-page',
  templateUrl: './contacts-page.component.html',
  styleUrls: ['./contacts-page.component.scss'],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})
export class ContactsPageComponent implements OnInit, OnDestroy {
  contacts = [];
  filteredContacts = [];

  dialogRef: any;
  hasSelectedContacts: boolean;
  searchInput: FormControl;
  private readonly onDestroy: Subject<any> = new Subject<any>();

  // Private
  private _unsubscribeAll: Subject<any>;
  constructor(
    private _contactsService: ContactsService,
    private _fuseSidebarService: FuseSidebarService, private store: Store<AppState>,
    private _matDialog: MatDialog) {
    this.store.dispatch(loadPhoneBook({ email: 'dirkdhure%40gmail.com' }));
    // Set the defaults
    this.searchInput = new FormControl('');

    // Set the private defaults
    this._unsubscribeAll = new Subject();
  }

  ngOnInit(): any {
    this.store.pipe(select(selectContacts())).subscribe(contacts => {
      if (contacts) {
        this.contacts = contacts;
        this.filteredContacts = contacts;
      }
    });
  }
  /**
   * On destroy
   */
  ngOnDestroy(): void {
    this.onDestroy.next();
  }

  // -----------------------------------------------------------------------------------------------------
  // @ Public methods
  // -----------------------------------------------------------------------------------------------------

  /**
   * New contact
   */
  newContact(): void {
    this.dialogRef = this._matDialog.open(ContactFormComponent, {
      panelClass: 'contact-form-dialog',
      data: {
        action: 'new'
      }
    });
  }

  /**
   * Toggle the sidebar
   *
   * @param name
   */
  toggleSidebar(name): void {
    this._fuseSidebarService.getSidebar(name).toggleOpen();
  }

}
