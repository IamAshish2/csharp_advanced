import { port } from '@/global/config';
import {
  Mail,
  Lock,
  Eye,
  MessageSquare,
} from 'lucide-react';

const SignInPage = () => {

  function handleGoogleLogin() {
    window.location.href = `https://localhost:${port}/api/account/login/google`;
  }

  return (
    <div className="flex h-screen w-full font-sans">
      {/* Left Panel */}
      <div className="hidden md:flex md:w-1/2 bg-[#e2efec] flex-col justify-center items-center p-8">
        <div className="bg-white w-[170px] h-[170px] rounded-xl shadow-lg flex justify-center items-center mb-8">
          <div className="w-4/5 h-4/5 flex flex-col justify-center">
            <div className="h-4 bg-[#ffd9dc] rounded-lg my-2"></div>
            <div className="h-4 bg-[#ffd9dc] rounded-lg my-2"></div>
            <div className="h-4 bg-[#ffd9dc] rounded-lg my-2"></div>
          </div>
        </div>
        <div className="text-center max-w-[80%]">
          <h2 className="text-2xl font-semibold text-gray-800 mb-4">Welcome to ChatFlow</h2>
          <p className="text-gray-600 leading-relaxed">
            Connect, communicate, and collaborate with your team in a beautifully designed chat environment.
          </p>
        </div>
      </div>

      {/* Right Panel */}
      <div className="w-full md:w-1/2 flex justify-center items-center p-4 md:p-8 bg-white">
        <div className="w-full max-w-md">
          {/* Icon Header */}
          <div className="flex justify-center mb-6">
            <div className="bg-[#e2efec] w-12 h-12 rounded-lg flex justify-center items-center">
              <MessageSquare className="text-[#346a5f]" size={24} />
            </div>
          </div>

          {/* Form Header */}
          <h1 className="text-2xl font-semibold text-center text-gray-800 mb-2">Sign In</h1>
          <p className="text-sm text-center text-gray-600 mb-8">
            Welcome back! Please sign in to your account
          </p>

          {/* Form */}
          <form>
            {/* Email Field */}
            <div className="mb-6">
              <label htmlFor="email" className="block text-sm text-gray-800 mb-2">
                Email Address
              </label>
              <div className="relative">
                <div className="absolute inset-y-0 left-0 pl-3 flex items-center pointer-events-none">
                  <Mail className="text-gray-400" size={20} />
                </div>
                <input
                  type="email"
                  id="email"
                  placeholder="Enter your email"
                  className="w-full pl-10 pr-3 py-3 border border-gray-200 rounded-md focus:outline-none focus:ring-2 focus:ring-[#346a5f] focus:ring-opacity-20 focus:border-[#346a5f] transition-colors"
                />
              </div>
            </div>

            {/* Password Field */}
            <div className="mb-6">
              <label htmlFor="password" className="block text-sm text-gray-800 mb-2">
                Password
              </label>
              <div className="relative">
                <div className="absolute inset-y-0 left-0 pl-3 flex items-center pointer-events-none">
                  <Lock className="text-gray-400" size={20} />
                </div>
                <input
                  type="password"
                  id="password"
                  placeholder="Enter your password"
                  className="w-full pl-10 pr-10 py-3 border border-gray-200 rounded-md focus:outline-none focus:ring-2 focus:ring-[#346a5f] focus:ring-opacity-20 focus:border-[#346a5f] transition-colors"
                />
                <div className="absolute inset-y-0 right-0 pr-3 flex items-center">
                  <Eye className="text-gray-400 cursor-pointer" size={20} />
                </div>
              </div>
            </div>

            {/* Remember Me & Forgot Password */}
            <div className="flex justify-between items-center mb-6">
              <label className="flex items-center">
                <input
                  type="checkbox"
                  className="w-4 h-4 mr-2 accent-[#346a5f]"
                />
                <span className="text-sm text-gray-600">Remember me</span>
              </label>
              <a href="#" className="text-sm text-[#346a5f] hover:underline">
                Forgot Password?
              </a>
            </div>

            {/* Sign In Button */}
            <button
              type="submit"
              className="w-full bg-[#346a5f] text-white py-3 rounded-md font-medium hover:bg-[#2b574d] transition-colors mb-6"
            >
              Sign In
            </button>

            {/* Separator */}
            <div className="relative flex items-center mb-6">
              <div className="flex-grow border-t border-gray-200"></div>
              <span className="flex-shrink mx-4 text-sm text-gray-500">Or continue with</span>
              <div className="flex-grow border-t border-gray-200"></div>
            </div>

            {/* Social Login */}
            <div className="flex gap-4 mb-8">
              <button
                type="button"
                onClick={() => { handleGoogleLogin() }}
                className="flex-1 flex justify-center items-center gap-2 border border-gray-200 py-2.5 rounded-md hover:bg-gray-50 transition-colors"
              >
                {/* Google Icon (simplified) */}
                <svg width="20" height="20" viewBox="0 0 20 20" fill="none" xmlns="http://www.w3.org/2000/svg">
                  <path d="M19.6053 10.227C19.6053 9.49813 19.5467 8.89079 19.4207 8.26172H10V12.0508H15.4733C15.3767 12.8344 14.7973 14.0117 13.6067 14.7859L13.5869 14.9223L16.5864 17.2984L16.8004 17.3203C18.7027 15.5953 19.6053 13.1328 19.6053 10.227Z" fill="#4285F4" />
                  <path d="M10.0016 20C12.7036 20 14.9676 19.1047 16.802 17.3203L13.6083 14.7859C12.7623 15.3688 11.5889 15.7727 10.0016 15.7727C7.3716 15.7727 5.14827 14.0602 4.35827 11.6984L4.22888 11.709L1.11652 14.1793L1.07495 14.3031C2.89764 17.7578 6.20291 20 10.0016 20Z" fill="#34A853" />
                  <path d="M4.35667 11.6984C4.14667 11.0922 4.02667 10.4391 4.02667 9.76562C4.02667 9.09218 4.14667 8.43906 4.34667 7.83281L4.34043 7.6883L1.19711 5.18548L1.07336 5.22812C0.38862 6.58437 0 8.12343 0 9.76562C0 11.4078 0.38862 12.9469 1.07336 14.3031L4.35667 11.6984Z" fill="#FBBC05" />
                  <path d="M10.0016 3.75781C11.8896 3.75781 13.1836 4.55156 13.9136 5.22812L16.7722 2.43593C14.9589 0.772656 12.7036 0 10.0016 0C6.20291 0 2.89764 2.24218 1.07495 5.69687L4.34825 8.3016C5.14825 5.93984 7.3716 3.75781 10.0016 3.75781Z" fill="#EB4335" />
                </svg>
                <span className="text-sm">Google</span>
              </button>
              <button
                type="button"
                className="flex-1 flex justify-center items-center gap-2 border border-gray-200 py-2.5 rounded-md hover:bg-gray-50 transition-colors"
              >
                {/* Microsoft Icon (simplified) */}
                <svg width="20" height="20" viewBox="0 0 20 20" fill="none" xmlns="http://www.w3.org/2000/svg">
                  <path d="M9.5238 9.5238H0V0H9.5238V9.5238Z" fill="#F25022" />
                  <path d="M20.0001 9.5238H10.4763V0H20.0001V9.5238Z" fill="#7FBA00" />
                  <path d="M9.5238 20H0V10.4762H9.5238V20Z" fill="#00A4EF" />
                  <path d="M20.0001 20H10.4763V10.4762H20.0001V20Z" fill="#FFB900" />
                </svg>
                <span className="text-sm">Microsoft</span>
              </button>
            </div>

            {/* Sign Up Link */}
            <div className="text-center">
              <p className="text-gray-600 text-sm">
                Don't have an account? <a href="#" className="text-[#346a5f] font-medium hover:underline">Sign up</a>
              </p>
            </div>
          </form>
        </div>
      </div>
    </div>
  );
};

export default SignInPage;