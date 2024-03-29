import { GoogleLogin, GoogleOAuthProvider } from "@react-oauth/google";
import oathswornlogo from "../assets/oathswornlogo.png";
import Cookies from 'js-cookie';
import { useNavigate } from "react-router-dom";

export const SplashScreen = () => {
  const navigate = useNavigate();

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
                if (credentialResponse.credential) {
                  Cookies.set('Authorization', credentialResponse.credential, { secure: true });
                  navigate('/main-menu');
                }
              }}
              onError={() => {
                console.log('Login Failed');
              }}
              useOneTap
            />
          </div>
        </div>
      </div>
    </GoogleOAuthProvider>
  );
};