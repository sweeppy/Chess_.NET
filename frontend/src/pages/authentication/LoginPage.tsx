import { ChangeEvent, useState } from 'react'
import { isUserExistsAndEmailConfirmedAsync } from '../../services/Auth/IsUserExists'
import { AddNewUserAndSendVerificationCodeAsync } from '../../services/Auth/AddNewUser'
import { VerifyCodeAsync } from '../../services/Auth/VerifyCode'
import { AlternativeLogin } from '../../components/authentication/AlternativeLogin'
import { SendVerificationCodeAsync } from '../../services/Auth/SendVerificationCode'
import { useNavigate } from 'react-router-dom'
import ErrorAlert from '../../components/alerts/ErrorAlert'

const LoginPage = () => {
    const navigate = useNavigate()

    // When user is not registered - verification section
    const [isVerificationSectionVisible, setIsVerificationSectionVisible] =
        useState(false)
    // When user is registered - password section
    const [isPasswordSectionVisible, setIsPasswordSectionVisible] =
        useState(false)

    // Error message state
    const [errorAlertMessage, setErrorAlertMessage] = useState<string | null>(
        null
    )

    // Handle alert animation
    const [isAlertClosing, setIsAlertClosing] = useState(false)
    const closeAlert = () => {
        setIsAlertClosing(true) // Start animation timeout
        setTimeout(() => {
            setErrorAlertMessage(null)
            setIsAlertClosing(false)
        }, 500)
    }

    // Inputs
    const [emailText, setEmailText] = useState('')
    const [codeText, setCodeText] = useState('')
    const [passwordText, setPasswordText] = useState('')
    const handleEmailTextChange = (e: ChangeEvent<HTMLInputElement>) => {
        setEmailText(e.target.value)
        if (isVerificationSectionVisible || isPasswordSectionVisible) {
            setIsVerificationSectionVisible(false)
            setIsPasswordSectionVisible(false)
            setCodeText('')
        }
    }

    // State for email sending loader
    const [isSending, setIsSending] = useState(false)

    // After email input and continue with password button click
    const handleEmailContinueClick = async () => {
        try {
            setIsSending(true)
            const data = await isUserExistsAndEmailConfirmedAsync(emailText)

            if (data.isAccountCreated) {
                // if user have already created his account
                setIsPasswordSectionVisible(true)
            } else if (data.isConfirmed) {
                // if user only confirm his email, but didn't create his account
                navigate('/createAccount')
            } else if (data.isExists) {
                // is user already in DB, but didn't confirm his email
                await SendVerificationCodeAsync(emailText)
                setIsVerificationSectionVisible(true)
            } else {
                // user not in DB
                await AddNewUserAndSendVerificationCodeAsync(emailText)
                setIsVerificationSectionVisible(true)
            }
        } catch (error: any) {
            setErrorAlertMessage(error.message)
        } finally {
            setIsSending(false)
        }
    }

    // After password input and continue button click
    const handleLoginByPassword = async () => {}

    // After verification code input and continue button click
    const handleVerifyCodeAsync = async () => {
        try {
            const data = await VerifyCodeAsync(emailText, codeText)
            if (data.isCodeCorrect == false) {
                setErrorAlertMessage('Wrong code!')
            } else {
                navigate('/createAccount')
            }
        } catch (error: any) {
            setErrorAlertMessage(error.message)
        }
    }

    return (
        <>
            {errorAlertMessage && (
                <ErrorAlert
                    isAlertClosing={isAlertClosing}
                    errorMessage={errorAlertMessage}
                    closeAlert={closeAlert}
                />
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
                        isSending ||
                        isVerificationSectionVisible ||
                        isPasswordSectionVisible
                    }
                >
                    {isSending ? 'Sending...' : 'Continue'}
                </button>
                {isSending && <span className="loader"></span>}
                {isVerificationSectionVisible && (
                    <>
                        <div className="max-width padding-block-600">
                            <p className="fs-xxs padding-block-400">
                                Please confirm your email to continue. Check
                                your inbox for a verification code and enter it
                                below.
                            </p>
                            <label className="label">Verification Code</label>
                            <div className="input-container">
                                <input
                                    className="input"
                                    type="text"
                                    placeholder="Paste verification code "
                                    value={codeText}
                                    onChange={(e) =>
                                        setCodeText(e.target.value)
                                    }
                                />
                            </div>
                        </div>
                        <button
                            className="button max-width"
                            onClick={handleVerifyCodeAsync}
                        >
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
                                    onChange={(e) =>
                                        setPasswordText(e.target.value)
                                    }
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
    )
}

export default LoginPage
