import { toast, ToastPosition } from "react-toastify";

export const notify = {
  success(message: string, position: ToastPosition) {
    toast.success(message, {
      position,
    });
  },

  error(message: string, position: ToastPosition) {
    toast.error(message, {
      position,
      autoClose: 5000,
      hideProgressBar: false,
      closeOnClick: true,
      pauseOnHover: true,
      draggable: true,
      progress: undefined,
    });
  },

  warning(message: string, position: ToastPosition) {
    toast.warn(message, {
      position,
      autoClose: 5000,
      hideProgressBar: false,
      closeOnClick: true,
      pauseOnHover: true,
      draggable: true,
      progress: undefined,
    });
  },

  inform(message: string, position: ToastPosition) {
    toast.info(message, {
      position,
      autoClose: 5000,
      hideProgressBar: false,
      closeOnClick: true,
      pauseOnHover: true,
      draggable: true,
      progress: undefined,
    });
  },

  custom(message: string, position: ToastPosition, className: string) {
    toast(message, {
      position,
      autoClose: 5000,
      hideProgressBar: false,
      closeOnClick: true,
      pauseOnHover: true,
      draggable: true,
      progress: undefined,
      className,
    });
  },
};
