import { HttpClient } from "@angular/common/http";
import { Component, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";

@Component({
  selector: "app-login",
  templateUrl: "./login.component.html",
  styleUrls: ["./login.component.scss"],
})
export class LoginComponent implements OnInit {
  username: string = "";
  password: string = "";
  returnUrl: string = "";
  error: string = "";

  constructor(private http: HttpClient, private router: ActivatedRoute) {}

  ngOnInit(): void {
    this.returnUrl = this.router.snapshot.queryParams["ReturnUrl"];
  }

  login() {
    this.error = "";
    this.http
      .post("https://localhost:5003/auth/login", {
        username: this.username,
        password: this.password,
        rememberLogin: false,
        returnUrl: this.returnUrl,
      })
      .subscribe(
        (rsp) => {
          window.location.href = (rsp as any).returnUrl;
        },
        (_) => {
          this.error = `Login failed!`;
        }
      );
  }
}
