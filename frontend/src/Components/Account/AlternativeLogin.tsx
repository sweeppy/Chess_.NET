interface Props {
  className?: string;
}
export const AlternativeLogin = ({ className }: Props) => {
  return (
    <div className={`max-width ${className}`}>
      <button data-icon className="primary-button max-width">
        <img src="/design/web/icons/auth/google_icon.svg" alt="Google icon" />
        <div>Continue with Google</div>
      </button>
    </div>
  );
};
