import { ChangeEvent, useState } from "react";
import { isUserExistsAndEmailConfirmedAsync } from "../../Requests/Auth/IsUserExists";
import { AddNewUserAndSendVerificationCodeAsync } from "../../Requests/Auth/AddNewUser";
import { VerifyCodeAsync } from "../../Requests/Auth/VerifyCode";
import { AlternativeLogin } from "../../components/authentication/AlternativeLogin";

const LoginPage = () => {
  // When user is not registered - verification section
  const [isVerificationSectionVisible, setIsVerificationSectionVisible] =
    useState(false);
  // When user is registered - password section
  const [isPasswordSectionVisible, setIsPasswordSectionVisible] = useState(false);

  // Error state
  const [errorAlert, setErrorAlert] = useState<string | null>(null);

  // Handle alert animation
  const [isAlertClosing, setIsAlertClosing] = useState(false);
  const closeAlert = () => {
    setIsAlertClosing(true); // Start animation timeout
    setTimeout(() => {
      setErrorAlert(null);
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
      setCodeText("");
    }
  };
  // Verification code input
  const [codeText, setCodeText] = useState("");
  const handleCodeTextChange = (e: ChangeEvent<HTMLInputElement>) => {
    setCodeText(e.target.value);
  };

  // Password input
  const [passwordText, setPasswordText] = useState("");
  const handlePasswordTextChange = (e: ChangeEvent<HTMLInputElement>) => {
    setPasswordText(e.target.value);
  };

  // State for email sending loader
  const [isSending, setIsSending] = useState(false);

  // After email input and continue with password button click
  const handleEmailContinueClick = async () => {
    try {
      setIsSending(true);
      const isUserExists = await isUserExistsAndEmailConfirmedAsync(emailText);

      if (isUserExists) {
        setIsPasswordSectionVisible(true);
      } else {
        await AddNewUserAndSendVerificationCodeAsync(emailText);
        setIsVerificationSectionVisible(true);
      }
    } catch (error: any) {
      setErrorAlert(error.message);
    } finally {
      setIsSending(false);
    }
  };

  // After password input and continue button click
  const handleLoginByPassword = async () => {};

  // After verification code input and continue button click
  const handleVerifyCodeAsync = async () => {
    await VerifyCodeAsync(emailText, codeText);
  };

  return (
    <>
      {errorAlert && (
        <div className={`alert error-alert ${isAlertClosing ? "hide" : ""}`}>
          <span className="alert-message">{errorAlert}</span>
          <button className="close" onClick={() => closeAlert()}>
            &times;
          </button>
        </div>
      )}

      <div className="container auth-container">
        <h2 className="fs-xl fw-bold padding-block-900">Log in</h2>
        <AlternativeLogin />
        <div className="max-width padding-block-600">
          <label className="label">Email</label>
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
        <button
          className="button max-width"
          onClick={handleEmailContinueClick}
          disabled={
            isSending || isVerificationSectionVisible || isPasswordSectionVisible
          }
        >
          {isSending ? "Sending..." : "Continue"}
        </button>
        {isSending && <span className="loader"></span>}
        {isVerificationSectionVisible && (
          <>
            <div className="max-width padding-block-600">
              <p className="fs-xxs padding-block-400">
                Please confirm your email to continue. Check your inbox for a
                verification code and enter it below.
              </p>
              <label className="label">Verification Code</label>
              <div className="input-container">
                <input
                  className="input"
                  type="text"
                  placeholder="Paste verification code "
                  value={codeText}
                  onChange={handleCodeTextChange}
                />
              </div>
            </div>
            <button className="button max-width" onClick={handleVerifyCodeAsync}>
              Verify code
            </button>
          </>
        )}
        {isPasswordSectionVisible && (
          <>
            <div className="max-width padding-block-600">
              <label className="label">Password</label>
              <div className="input-container">
                <input
                  className="input"
                  type="password"
                  placeholder="Enter your password..."
                  onChange={handlePasswordTextChange}
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

export default LoginPage;
