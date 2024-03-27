import { GoogleLogin, GoogleOAuthProvider } from "@react-oauth/google";
import oathswornlogo from "../assets/oathswornlogo.png";
export const SplashScreen = () => {

  return (
    <GoogleOAuthProvider clientId={import.meta.env.VITE_GOOGLE_CLIENT_ID}>
      <div>
        <div className="flex flex-col caret-transparent">
          <div className="flex justify-center">
            <img src={oathswornlogo} alt="oathsworn logo" />
          </div>
          <div className="flex justify-center">
            <GoogleLogin
              onSuccess={credentialResponse => {
                console.log(credentialResponse);
              }}
              onError={() => {
                console.log('Login Failed');
              }}
            />
          </div>
        </div>
      </div>
    </GoogleOAuthProvider>
  );
};