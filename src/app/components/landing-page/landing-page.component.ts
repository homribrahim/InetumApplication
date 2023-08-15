import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-landing-page',
  templateUrl: './landing-page.component.html',
  styleUrls: ['./landing-page.component.scss'],
})
export class LandingPageComponent implements OnInit {
  constructor(private router: Router) {}
  showDiv: boolean = false;

  ngOnInit(): void {
    setTimeout(() => {
      this.showDiv = true;
    }, 2500); // 3000 milliseconds = 3 seconds
    setTimeout(() => {
      this.router.navigate(['login']);
    }, 9500);
  }
}
