interface Props {
  className?: string;
}
export const AlternativeLogin = ({ className }: Props) => {
  return (
    <div className={`max-width ${className}`}>
      <button data-icon className="button max-width">
        <img src="src/assets/web/icons/auth/google_icon.svg" alt="Google icon" />
        <div>Continue with Google</div>
      </button>
    </div>
  );
};
