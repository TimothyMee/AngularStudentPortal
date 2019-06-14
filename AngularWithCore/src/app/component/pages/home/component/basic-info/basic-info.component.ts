import { Component, OnInit, Input } from '@angular/core';
import { DashboardService } from 'src/app/services/dashboard.service';

@Component({
  selector: 'app-basic-info',
  templateUrl: './basic-info.component.html',
  styleUrls: ['./basic-info.component.css']
})
export class BasicInfoComponent implements OnInit {
  programme: string;
  level: number;
  @Input() matricNo: string;
  userName: string;
  pic: string;
  constructor(private profile: DashboardService) {
    console.log("mat",this.matricNo)
    this.matricNo=localStorage.getItem("mat");
  }

  ngOnInit() {
    this.profile.getProfile(this.matricNo).subscribe(res => {
      this.level = res.level;
      this.programme = res.programmeName;
      this.userName = res.fullName
    });
    
  }

  //receiveMessage($event) {
  //this.matricNo=$event}

}
