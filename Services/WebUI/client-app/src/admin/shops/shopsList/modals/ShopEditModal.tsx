import {Product} from "../../../../global/models/product";
import React, {useState} from "react";
import * as yup from "yup";
import {Form, Formik} from "formik";
import {Button, FormControl, InputLabel, MenuItem, Select, Stack, Switch, Typography} from "@mui/material";
import MyTextInput from "../../../../components/MyTextInput";
import PhotoUploadWidget from "../../../../components/photo/PhotoUploadWidget";
import LoadingComponent from "../../../../components/LoadingComponent";
import {Shop} from "../../../../global/models/shop";

interface Props {
    shop: Shop;
    onSubmit: (editedShop: Shop) => void;
}

const ShopEditModal: React.FC<Props> = ({shop, onSubmit}) => {
    const [loading, setLoading] = useState(false)

    const validationSchema = yup.object({
        name: yup.string().required( 'Nazwa produktu jest wymagana'),
        description: yup.string().required( 'Opis produktu jest wymagany'),
        subcategory: yup.string().required('Podkategoria jest wymagana'),
    });

    return (
        <Formik
            initialValues={shop}
            onSubmit={(values) => onSubmit(values)}
            validateOnMount={true} validationSchema={validationSchema}
        >
            {({ handleSubmit, values, handleChange, isValid }) => (
                <Form>
                    <Stack direction={'column'} gap={3} style={{display: 'flex', justifyContent: 'center', alignItems: 'center'}}>
                        <Typography variant="h4" style={{display: 'flex', justifyContent: 'space-between', alignItems: 'center'}} height={'75px'}>{shop.shopName}</Typography>
                        <MyTextInput name='description' placeholder={'Opis'} label='Opis' />
                        <FormControl fullWidth >
                            <InputLabel>Kategoria</InputLabel>
                            <Select
                                id={'category'}
                                name={'category.value'}
                                label="Kategoria"
                                onChange={handleChange}
                            >
                                <MenuItem value='Wędlina'>Wędlina</MenuItem>
                                <MenuItem value='Mięso'>Mięso</MenuItem>
                            </Select>
                        </FormControl>
                        <MyTextInput name='subcategory' placeholder={'Podkategoria'} label='Podkategoria' />
                        <Stack direction={'row'} spacing={2} style={{display: 'flex', justifyContent: 'start', alignItems: 'center'}}>
                            <Typography variant={'h6'}>Jednostka alternatywna?</Typography>
                            <Switch
                                name={'details.isWeightSwitchAllowed'}
                                checked={false}
                                onChange={handleChange}
                            />
                        </Stack>
                        <Button
                            disabled={!isValid || loading}
                            type={'submit'} onClick={() => handleSubmit} variant={'contained'}>
                            {loading ?
                                <LoadingComponent /> : <Typography>Zaakceptuj zmiany</Typography>
                            }
                        </Button>
                    </Stack>
                </Form>
            )}
        </Formik>
    );
}

export default ShopEditModal