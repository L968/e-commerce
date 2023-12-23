import { CrudType } from '@/interfaces/CrudType';
import React, { createContext, useContext, ReactNode, FC, useState, Dispatch, SetStateAction } from 'react';

interface ProductContextProps {
  activeStep: number
  next: () => void
  back: () => void
  crudType: CrudType | null
  setCrudType: Dispatch<SetStateAction<CrudType | null>>
  productId: string
  setProductId: Dispatch<SetStateAction<string>>
  productCategoryId: string
  setProductCategoryId: Dispatch<SetStateAction<string>>
}

const ProductContext = createContext<ProductContextProps | undefined>(undefined);

export const ProductProvider: FC<{ children: ReactNode }> = ({ children }) => {
  const [activeStep, setActiveStep] = useState<number>(0);
  const [crudType, setCrudType] = useState<CrudType | null>(null);
  const [productId, setProductId] = useState<string>('');
  const [productCategoryId, setProductCategoryId] = useState<string>('');

  const contextValue: ProductContextProps = {
    activeStep,
    next: () => { setActiveStep(prevStep => prevStep + 1) },
    back: () => { setActiveStep(prevStep => prevStep - 1) },
    crudType,
    setCrudType,
    productId,
    setProductId,
    productCategoryId,
    setProductCategoryId,
  };

  return (
    <ProductContext.Provider value={contextValue}>
      {children}
    </ProductContext.Provider>
  )
}

export const useProductContext = () => {
  const context = useContext(ProductContext);

  if (!context) {
    throw new Error('useProductContext must be used within a ProductProvider');
  }

  return context;
}
