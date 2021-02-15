import { DataSource } from '@angular/cdk/table';
import { Component, Input, OnChanges, OnDestroy, OnInit, SimpleChanges, TemplateRef, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatDialog, MatDialogRef, MatPaginator, MatSort, MatTableDataSource } from '@angular/material';
import { fuseAnimations } from '@fuse/animations';
import { FuseConfirmDialogComponent } from '@fuse/components/confirm-dialog/confirm-dialog.component';
import { FuseSidebarService } from '@fuse/components/sidebar/sidebar.service';
import { Store } from '@ngrx/store';
import { AppState } from 'app/store/reducers';
import { Observable, Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { ContactFormComponent } from '../contact-form/contact-form.component';
import { ContactsService } from '../services/contacts.service';

@Component({
    selector: 'app-contacts-list',
    templateUrl: './contacts-list.component.html',
    styleUrls: ['./contacts-list.component.scss'],
    encapsulation: ViewEncapsulation.None,
    animations: fuseAnimations
})
export class ContactsListComponent implements OnInit, OnChanges {
    displayedColumns = ['avatar', 'name', 'contactType', 'contacts', 'buttons'];
    @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
    @ViewChild(MatSort, { static: true }) sort: MatSort;
    confirmDialogRef: MatDialogRef<FuseConfirmDialogComponent>;

    dataSource = new MatTableDataSource([]);
    obs: Observable<any>;

    @Input() contacts;
    dialogRef: any;

    constructor(
        private _matDialog: MatDialog,
        private store: Store<AppState>,
        private _contactsService: ContactsService,
        private _fuseSidebarService: FuseSidebarService) {

    }

    ngOnInit(): any {
        this.dataSource = new MatTableDataSource(this.contacts);
        this.obs = this.dataSource.connect();

        this._contactsService.onSearchTextChanged.subscribe(filterValue => {
            filterValue = filterValue.trim().toLowerCase();
            this.dataSource.filter = filterValue;
        });
    }


    ngOnChanges(changes: SimpleChanges): void {
        if (changes.contacts.previousValue !== changes.contacts.currentValue) {
            this.dataSource = new MatTableDataSource(changes.contacts.currentValue);
            this.dataSource.paginator = this.paginator;
            this.dataSource.sort = this.sort;
        }
    }

    addContact(contact): any {
        this.dialogRef = this._matDialog.open(ContactFormComponent, {
            panelClass: 'contact-form-dialog',
            data: {
                action: 'new'
            }
        });
    }

    deleteContact(contactId): void {
        this.confirmDialogRef = this._matDialog.open(FuseConfirmDialogComponent, {
            disableClose: false
        });

        this.confirmDialogRef.componentInstance.confirmMessage = 'Are you sure you want to delete?';

        this.confirmDialogRef.afterClosed().subscribe(result => {
            if ( result )
            {
            //    this._contactsService.deleteContact(contact);
            }
            this.confirmDialogRef = null;
        });
    }

}
