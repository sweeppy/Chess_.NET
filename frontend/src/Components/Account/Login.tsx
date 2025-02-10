import { ChangeEvent, useState } from "react";
import { AlternativeLogin } from "./AlternativeLogin";
import { isUserExistsRequestAsync } from "../../Requests/Auth/IsUserExists";

const Login = () => {
  // When user is not registered - verification section
  const [isVerificationSectionVisible, setIsVerificationSectionVisible] =
    useState(false);
  // When user is registered - password section
  const [isPasswordSectionVisible, setIsPasswordSectionVisible] = useState(false);

  // Error state
  const [error, setError] = useState<string | null>(null);

  // Handle alert animation
  const [isAlertClosing, setIsAlertClosing] = useState(false);
  const closeAlert = () => {
    setIsAlertClosing(true); // Start animation timeout
    setTimeout(() => {
      setError(null);
      setIsAlertClosing(false);
    }, 500);
  };

  // Email input
  const [emailText, setEmailText] = useState("");
  const handleEmailTextChange = (e: ChangeEvent<HTMLInputElement>) => {
    setEmailText(e.target.value);
    if (isVerificationSectionVisible || isPasswordSectionVisible) {
      setIsVerificationSectionVisible(false);
      setIsPasswordSectionVisible(false);
    }
  };

  // After email input
  const handleEmailContinueClick = async () => {
    try {
      const isUserExists = await isUserExistsRequestAsync(emailText);

      if (isUserExists) {
        setIsPasswordSectionVisible(true);
      } else {
        setIsVerificationSectionVisible(true);
      }
    } catch (error: any) {
      setError(
        "En error occurred while checking your account. Please try again or contact support."
      );
    }
  };
  return (
    <>
      {error && (
        <div className={`alert error-alert ${isAlertClosing ? "hide" : ""}`}>
          <span className="alert-message">{error}</span>
          <button className="close" onClick={() => closeAlert()}>
            &times;
          </button>
        </div>
      )}

      <div className="container auth-container">
        <h2 className="fs-xl fw-bold padding-block-900">Log in</h2>
        <AlternativeLogin />
        <div className="max-width padding-block-600">
          <label htmlFor="" className="label">
            Email
          </label>
          <div className="input-container">
            <input
              className="input"
              type="email"
              placeholder="Enter you email..."
              value={emailText}
              onChange={handleEmailTextChange}
            />
          </div>
        </div>
        <button className="button max-width" onClick={handleEmailContinueClick}>
          Continue
        </button>
        {isVerificationSectionVisible && (
          <>
            <div className="max-width padding-block-600">
              <p className="fs-xxs padding-block-400">
                Please confirm your email to continue. Check your inbox for a
                verification code and enter it below.
              </p>
              <label htmlFor="" className="label">
                Verification Code
              </label>
              <div className="input-container">
                <input
                  className="input"
                  type="text"
                  placeholder="Paste verification code "
                />
              </div>
            </div>
            <button className="button max-width">Verify code</button>
          </>
        )}
        {isPasswordSectionVisible && (
          <>
            <div className="max-width padding-block-600">
              <label htmlFor="" className="label">
                Password
              </label>
              <div className="input-container">
                <input
                  className="input"
                  type="password"
                  placeholder="Enter your password..."
                />
              </div>
            </div>
            <button className="button max-width no-wrap">
              Continue with password
            </button>
          </>
        )}
      </div>
    </>
  );
};

export default Login;
