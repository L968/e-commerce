import Autocomplete from "@/components/Autocomplete";
import GetVariantsResponse from "@/interfaces/api/responses/GetVariantsResponse";

interface VariantSelection {
    variantId: number;
    optionIds: number[];
}

interface VariantFormProps {
    variants: GetVariantsResponse[];
    //onSubmit: (selections: VariantSelection[]) => void;
}

export default function VariantForm({ variants }: VariantFormProps) {
    return (
        <>
            <Autocomplete
                label='Variant'
                options={variants}
            />
        </>
    )
}
