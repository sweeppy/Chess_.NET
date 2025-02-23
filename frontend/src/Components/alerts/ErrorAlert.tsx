interface Props {
  isAlertClosing: boolean;
  errorMessage: string;
  closeAlert: () => void;
}

const ErrorAlert = (props: Props) => {
  return (
    <div className={`alert error-alert ${props.isAlertClosing ? "hide" : ""}`}>
      <span className="alert-message">{props.errorMessage}</span>
      <button className="close" onClick={() => props.closeAlert()}>
        &times;
      </button>
    </div>
  );
};

export default ErrorAlert;
