import React, {useState} from 'react';
import { Formik, Field, FieldProps, Form } from 'formik';
import {
  Button,
  TextField,
  FormControl,
  InputLabel,
  Select,
  MenuItem,
  Typography,
  IconButton,
  Box,
  Stack, Switch,
} from '@mui/material';
import { Category, Product } from '../../../global/models/product';
import MyTextInput from '../../../components/MyTextInput';
import {values} from "mobx";
import PhotoUploadWidget from "../../../components/photo/PhotoUploadWidget";
import LoadingComponent from "../../../components/LoadingComponent";
import * as yup from "yup";

interface Props {
  product: Product;
  onSubmit: (editedProduct: Product) => void;
  onPhotoChange: (photo: Blob) => void
}

const ProductEditModal: React.FC<Props> = ({product, onSubmit, onPhotoChange}) => {
  const [loading, setLoading] = useState(false)

  const validationSchema = yup.object({
    name: yup.string().required( 'Nazwa produktu jest wymagana'),
    description: yup.string().required( 'Opis produktu jest wymagany'),
    subcategory: yup.string().required('Podkategoria jest wymagana'),
  });

  const handleSubmit = (product: Product) => {
    //TEMP
    product.subcategory = ''
    onSubmit(product)
  }

  return (
    <Formik
      initialValues={product}
      onSubmit={(values) => handleSubmit(values)}
      validateOnMount={true} validationSchema={validationSchema}
    >
      {({ handleSubmit, values, handleChange, isValid }) => (
        <Form>
          <Stack direction={'column'} gap={3} style={{display: 'flex', justifyContent: 'center', alignItems: 'center'}}>
            <Stack direction={'row'} gap={2} style={{display: 'flex', justifyContent: 'space-between', alignItems: 'center'}}>
              <img src={values.image.url}  alt={values.name} style={{width: '75px', height: '75px', borderRadius: '15px'}} />
              <Typography variant="h4" style={{display: 'flex', justifyContent: 'space-between', alignItems: 'center'}} height={'75px'}>{product.name}</Typography>
            </Stack>
            <MyTextInput name='description' placeholder={'Opis'} label='Opis' />
            <FormControl fullWidth >
              <InputLabel>Kategoria</InputLabel>
              <Select
                  id={'category'}
                  name={'category.value'}
                  value={values.category.value}
                  label="Kategoria"
                  onChange={handleChange}
              >
                <MenuItem value='Wędlina'>Wędlina</MenuItem>
                <MenuItem value='Mięso'>Mięso</MenuItem>
              </Select>
            </FormControl>
            {/*<MyTextInput name='subcategory' placeholder={'Podkategoria'} label='Podkategoria'/>*/}
            <Stack direction={'row'} spacing={2} style={{display: 'flex', justifyContent: 'start', alignItems: 'center'}}>
              <Typography variant={'h6'}>Jednostka alternatywna?</Typography>
              <Switch
                  name={'details.isWeightSwitchAllowed'}
                  checked={values.details.isWeightSwitchAllowed}
                  onChange={handleChange}
              />
            </Stack>
            {values.details.isWeightSwitchAllowed &&
                <MyTextInput
                    placeholder={'Waga jednostkowa'}
                    name={'details.singleWeight.value'}
                    showErrors
                    inputProps={{value: values.details.singleWeight ? values.details.singleWeight.value : 0}}
                    type={'number'}
                />
            }
            <PhotoUploadWidget uploadPhoto={(file) => onPhotoChange(file)} setLoading={(state) => setLoading(state)} />
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
};

export default ProductEditModal;
