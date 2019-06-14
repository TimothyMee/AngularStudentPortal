import { Component, OnInit, Input } from '@angular/core';
import { DashboardService } from 'src/app/services/dashboard.service';
import { DataService } from 'src/app/services/data.service';

@Component({
  selector: 'app-basic-info',
  templateUrl: './basic-info.component.html',
  styleUrls: ['./basic-info.component.css']
})
export class BasicInfoComponent implements OnInit {
  programme: string;
  level: number;
 /* @Input() */matricNo: string;
  userName: string;
  pic: string;
  constructor(private profile: DashboardService, private dataService: DataService) {

    console.log("mat",this.matricNo)
    this.dataService.matricno.subscribe(matricno => this.matricNo = matricno);
  }

  ngOnInit() {
    this.dataService.matricno.subscribe(matricno => this.matricNo = matricno);

    this.profile.getProfile(this.matricNo).subscribe(res => {
      this.level = res.level;
      this.programme = res.programmeName;
      this.userName = res.fullName
    });
    
  }

  //receiveMessage($event) {
  //this.matricNo=$event}

}
