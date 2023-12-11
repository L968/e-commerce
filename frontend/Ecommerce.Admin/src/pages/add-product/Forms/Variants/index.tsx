import api from '@/services/api';
import { Form } from "../../styles";
import { BaseFormProps } from "../..";
import { toast } from 'react-toastify';
import { useEffect, useState } from "react"
import GetVariantsResponse from "@/interfaces/api/responses/GetVariantsResponse"
import VariantForm from './VariantForm';

export default function Variants({ next }: BaseFormProps) {
    const [variants, setVariants] = useState<GetVariantsResponse[]>([]);

    useEffect(() => {
        api.get<GetVariantsResponse[]>('/variant')
            .then(response => setVariants(response.data))
            .catch(error => toast.error('Error 500'));
    }, []);

    return (
        <Form>
            <VariantForm variants={variants} />
        </Form>
    )
}
