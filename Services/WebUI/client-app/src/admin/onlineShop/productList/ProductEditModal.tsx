import React from 'react';
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
  Stack,
} from '@mui/material';
import { Category, Product } from '../../../global/models/product';
import MyTextInput from '../../../components/MyTextInput';

interface Props {
  product: Product;
  onSubmit: (editedProduct: Product) => void;
  onDelete: () => void;
}

const ProductEditModal: React.FC<Props> = ({product, onSubmit, onDelete}) => {

  return (
    <Formik
      initialValues={product}
      onSubmit={(values) => {
        console.log('Updated Product:', values);
        onSubmit(values);
      }}
    >
      {({ handleSubmit }) => (
        <Form>
          <Stack direction={'column'} gap={5}>
            <Typography variant="h4" textAlign={'center'} marginBottom={2}>{product.name}</Typography>
            <MyTextInput name='name' placeholder={'Nazwa produktu'} label='Nazwa produktu'  />
            <MyTextInput name='description' placeholder={'Opis'} label='Opis' />
            <FormControl fullWidth variant="outlined">
              <InputLabel>Kategoria</InputLabel>
              <Field as="select" name="category.value">
                {({ field }: FieldProps<string>) => (
                  <Select
                    {...field}
                    label="Kategoria"
                  >
                    <MenuItem value="Wędlina" style={{ textAlign: 'center' }}>
                      Wędlina
                    </MenuItem>
                    <MenuItem value="Mięso" style={{ textAlign: 'center' }}>
                      Mięso
                    </MenuItem>
                  </Select>
                )}
              </Field>
            </FormControl>
            <Stack direction={'row'} gap={5} style={{justifyContent:'center'}}>
              <Button
                variant="contained"
                color='success'
                onClick={() => handleSubmit()}
              >
                Akceptuj
              </Button>
              <Button
                variant="contained"
                color="primary"
                onClick={() => onDelete()}
              >
                Usuń
              </Button>
            </Stack>
          </Stack>
        </Form>
      )}
    </Formik>
  );
};

export default ProductEditModal;
