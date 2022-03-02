import { HttpClient } from "@angular/common/http";
import { Component, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import {GoogleLoginProvider, SocialAuthService} from "angularx-social-login";

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

  isLoggedin?: boolean = undefined;

  constructor(private http: HttpClient, private router: ActivatedRoute,
              private socialAuthService: SocialAuthService) {}

  ngOnInit(): void {
    this.returnUrl = this.router.snapshot.queryParams["ReturnUrl"];
  }

  login() {
    this.error = "";
    this.http
      .post("https://localhost:5003/Auth/login", {
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

  signup() {
    this.error = "";
    this.http
      .post("https://localhost:5003/Auth/signup", {
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

  googleLogin() {
    this.socialAuthService.signIn(GoogleLoginProvider.PROVIDER_ID);
  }

  googleSignOut(): void {
    this.socialAuthService.signOut();
  }
}
