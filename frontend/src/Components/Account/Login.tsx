import { AlternativeLogin } from "./AlternativeLogin";

const Login = () => {
  return (
    <div className="container auth-container">
      <h2 className="fs-xl fw-bold padding-block-900">Log in</h2>
      <AlternativeLogin />
      <div className="max-width padding-block-300">
        <label htmlFor="" className="label">
          Email
        </label>
        <div className="input-container">
          <input className="input" type="email" />
        </div>
      </div>
      <button className="common-button max-width">Continue</button>
      <div className="max-width padding-block-300">
        <label htmlFor="" className="label">
          Verification Code
        </label>
        <div className="input-container">
          <input className="input" type="text" />
        </div>
      </div>
      <button className="common-button max-width">Verify code</button>
      <div className="max-width padding-block-300">
        <label htmlFor="" className="label">
          Password
        </label>
        <div className="input-container">
          <input className="input" type="password" />
        </div>
      </div>
      <button className="common-button max-width">
        Continue with passowrd
      </button>
    </div>
  );
};

export default Login;
