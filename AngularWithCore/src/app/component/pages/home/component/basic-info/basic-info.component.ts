import { Component, OnInit } from '@angular/core';
import { DashboardService } from 'src/app/services/dashboard.service';

@Component({
  selector: 'app-basic-info',
  templateUrl: './basic-info.component.html',
  styleUrls: ['./basic-info.component.css']
})
export class BasicInfoComponent implements OnInit {
  programme: string;
  level: number;
  matricNo: string;
  userName: string;
  pic: string;

  constructor(private profile: DashboardService) {
    this.matricNo = '110408010';
  }

  ngOnInit() {
    this.profile.getProfile(this.matricNo).subscribe(res => {
      debugger;
      this.level = res.level;
      this.programme = res.programmeName;
      this.userName = res.fullName
    });
    
  }

}
