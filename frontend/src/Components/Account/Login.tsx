import { AlternativeLogin } from "./AlternativeLogin";

const Login = () => {
  return (
    <div className="container column-container">
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
          />
        </div>
      </div>
      <button className="button max-width">Continue</button>
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
      <button className="button max-width">Continue with passowrd</button>
    </div>
  );
};

export default Login;
