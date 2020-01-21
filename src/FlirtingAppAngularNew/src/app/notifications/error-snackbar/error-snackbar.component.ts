import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-error-snackbar',
  templateUrl: './error-snackbar.component.html',
  styleUrls: ['./error-snackbar.component.scss']
})
export class ErrorSnackbarComponent implements OnInit {
  @Input() message = '';
  constructor() { }

  ngOnInit() {
  }

}
