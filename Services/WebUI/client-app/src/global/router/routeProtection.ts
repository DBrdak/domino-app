import { useEffect } from 'react';
import { useNavigate } from 'react-router-dom';

export const usePreventNavigation = (forms: any[], redirectionPath: string) => {
  const navigate = useNavigate()
  useEffect(() => {
    if(forms.some(f => f === null)) {
      navigate(redirectionPath)
    }
  }, []);
};